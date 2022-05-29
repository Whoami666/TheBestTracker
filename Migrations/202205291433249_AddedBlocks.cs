namespace TheBestTracker.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedBlocks : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TimeBlocks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Index = c.Int(nullable: false),
                        Time = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.TimeBlocks");
        }
    }
}
