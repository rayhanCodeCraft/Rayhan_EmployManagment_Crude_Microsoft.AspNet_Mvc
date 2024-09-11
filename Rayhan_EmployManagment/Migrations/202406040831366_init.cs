namespace Rayhan_EmployManagment.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Employees",
                c => new
                    {
                        EmployeeId = c.Int(nullable: false, identity: true),
                        EmployeeName = c.String(),
                        JoinDate = c.DateTime(nullable: false),
                        Picture = c.String(),
                        IsActive = c.Boolean(nullable: false),
                        Salary = c.Int(nullable: false),
                        Email = c.String(),
                    })
                .PrimaryKey(t => t.EmployeeId);
            
            CreateTable(
                "dbo.QualificationEntries",
                c => new
                    {
                        QualificationEntryID = c.Int(nullable: false, identity: true),
                        EmployeeId = c.Int(nullable: false),
                        QualificationId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.QualificationEntryID)
                .ForeignKey("dbo.Employees", t => t.EmployeeId, cascadeDelete: true)
                .ForeignKey("dbo.Qualifications", t => t.QualificationId, cascadeDelete: true)
                .Index(t => t.EmployeeId)
                .Index(t => t.QualificationId);
            
            CreateTable(
                "dbo.Qualifications",
                c => new
                    {
                        QualificationId = c.Int(nullable: false, identity: true),
                        QualifcationName = c.String(),
                    })
                .PrimaryKey(t => t.QualificationId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.QualificationEntries", "QualificationId", "dbo.Qualifications");
            DropForeignKey("dbo.QualificationEntries", "EmployeeId", "dbo.Employees");
            DropIndex("dbo.QualificationEntries", new[] { "QualificationId" });
            DropIndex("dbo.QualificationEntries", new[] { "EmployeeId" });
            DropTable("dbo.Qualifications");
            DropTable("dbo.QualificationEntries");
            DropTable("dbo.Employees");
        }
    }
}
