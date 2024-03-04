namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class createDb2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Flights",
                c => new
                    {
                        flight_id = c.Long(nullable: false, identity: true),
                        departure_time = c.DateTime(nullable: false),
                        arrival_time = c.DateTime(nullable: false),
                        airline_id = c.Long(nullable: false),
                        route_id = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.flight_id)
                .ForeignKey("dbo.Routes", t => t.route_id, cascadeDelete: true)
                .Index(t => t.route_id);
            
            CreateTable(
                "dbo.Routes",
                c => new
                    {
                        route_id = c.Long(nullable: false, identity: true),
                        origin_city_id = c.Long(nullable: false),
                        destination_city_id = c.Long(nullable: false),
                        departure_date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.route_id);
            
            CreateTable(
                "dbo.Subscriptions",
                c => new
                    {
                        subscription_id = c.Long(nullable: false, identity: true),
                        agency_id = c.Long(nullable: false),
                        origin_city_id = c.Long(nullable: false),
                        destination_city_id = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.subscription_id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Flights", "route_id", "dbo.Routes");
            DropIndex("dbo.Flights", new[] { "route_id" });
            DropTable("dbo.Subscriptions");
            DropTable("dbo.Routes");
            DropTable("dbo.Flights");
        }
    }
}
