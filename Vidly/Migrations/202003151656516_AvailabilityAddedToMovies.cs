namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AvailabilityAddedToMovies : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Movies", "Availability", c => c.Int(nullable: false));
            Sql("UPDATE Movies SET Availability = NoInStock");
        }
        
        public override void Down()
        {
            DropColumn("dbo.Movies", "Availability");
        }
    }
}
