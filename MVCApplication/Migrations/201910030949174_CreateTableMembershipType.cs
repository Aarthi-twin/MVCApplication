namespace MVCApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateTableMembershipType : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MembershipTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Type = c.String(),
                        Duration = c.Short(nullable: false),
                        SignUpFree = c.Double(nullable: false),
                        Discount = c.Short(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Movies", "DateAdded", c => c.DateTime(nullable: false));
            DropColumn("dbo.Movies", "ActorName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Movies", "ActorName", c => c.String());
            DropColumn("dbo.Movies", "DateAdded");
            DropTable("dbo.MembershipTypes");
        }
    }
}
