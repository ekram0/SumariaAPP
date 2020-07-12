using Microsoft.EntityFrameworkCore.Migrations;

namespace EFCore.Data.Migrations
{
    public partial class SamuraiBattle : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"create function EarliestBattleFoughBySamurai (@samuraiId int)
				returns char(30) as
				Begin
					declare @ret char(30)
					select top 1 @ret=Name
				from Battles where Battles.Id
				in (select BattleID  from SamuraiBattle  where SamuriaID = @samuraiId)
				order by StartDate 
				return @ret
				end");

			migrationBuilder.Sql(@"create  view SamuraiBattleStats
				as
					select Samurais.Name ,
					COUNT(SamuraiBattle.BattleID) as NumberOfBattles,
					dbo.EarliestBattleFoughBySamurai(MIN(Samurais.Id)) as EarliestBattle
				From SamuraiBattle inner join Samurais
				on SamuraiBattle.SamuriaID = Samurais.Id
				group by Samurais.Name , SamuraiBattle.SamuriaID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
			migrationBuilder.Sql(@"drop view dbo.SamuraiBattleStats");
			migrationBuilder.Sql(@"drop view dbo.EarliestBattleFoughBySamurai");
		}
    }
}
