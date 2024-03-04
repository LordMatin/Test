namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateflightcol : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Flights", name: "routes_route_id", newName: "route_id_route_id");
            RenameIndex(table: "dbo.Flights", name: "IX_routes_route_id", newName: "IX_route_id_route_id");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Flights", name: "IX_route_id_route_id", newName: "IX_routes_route_id");
            RenameColumn(table: "dbo.Flights", name: "route_id_route_id", newName: "routes_route_id");
        }
    }
}
