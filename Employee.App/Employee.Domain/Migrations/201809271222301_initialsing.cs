namespace Employee.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initialsing : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Address",
                c => new
                    {
                        AddressId = c.Int(nullable: false, identity: true),
                        Line1 = c.String(),
                        Line2 = c.String(),
                        Line3 = c.String(),
                        City = c.String(),
                        State = c.String(),
                        Country = c.String(),
                        EmployeeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.AddressId)
                .ForeignKey("dbo.Employees", t => t.EmployeeId, cascadeDelete: true)
                .Index(t => t.EmployeeId);
            
            CreateTable(
                "dbo.Employees",
                c => new
                    {
                        EmployeeId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        BaseLocation = c.String(),
                    })
                .PrimaryKey(t => t.EmployeeId);
            
            CreateTable(
                "dbo.ProjectEngagements",
                c => new
                    {
                        EngagementId = c.Int(nullable: false, identity: true),
                        EmployeeId = c.Int(nullable: false),
                        ProjectId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.EngagementId)
                .ForeignKey("dbo.Employees", t => t.EmployeeId, cascadeDelete: true)
                .ForeignKey("dbo.Projects", t => t.ProjectId, cascadeDelete: true)
                .Index(t => t.EmployeeId)
                .Index(t => t.ProjectId);
            
            CreateTable(
                "dbo.Projects",
                c => new
                    {
                        ProjectId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.ProjectId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Address", "EmployeeId", "dbo.Employees");
            DropForeignKey("dbo.ProjectEngagements", "ProjectId", "dbo.Projects");
            DropForeignKey("dbo.ProjectEngagements", "EmployeeId", "dbo.Employees");
            DropIndex("dbo.ProjectEngagements", new[] { "ProjectId" });
            DropIndex("dbo.ProjectEngagements", new[] { "EmployeeId" });
            DropIndex("dbo.Address", new[] { "EmployeeId" });
            DropTable("dbo.Projects");
            DropTable("dbo.ProjectEngagements");
            DropTable("dbo.Employees");
            DropTable("dbo.Address");
        }
    }
}
