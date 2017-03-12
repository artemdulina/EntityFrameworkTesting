namespace ORM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Bolts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(unicode: false),
                        Wheel_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Wheels", t => t.Wheel_Id)
                .Index(t => t.Wheel_Id);
            
            CreateTable(
                "dbo.Wheels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Size = c.Double(nullable: false),
                        Car_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Cars", t => t.Car_Id)
                .Index(t => t.Car_Id);
            
            CreateTable(
                "dbo.Cars",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(unicode: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Wheels", "Car_Id", "dbo.Cars");
            DropForeignKey("dbo.Bolts", "Wheel_Id", "dbo.Wheels");
            DropIndex("dbo.Wheels", new[] { "Car_Id" });
            DropIndex("dbo.Bolts", new[] { "Wheel_Id" });
            DropTable("dbo.Cars");
            DropTable("dbo.Wheels");
            DropTable("dbo.Bolts");
        }
    }
}
