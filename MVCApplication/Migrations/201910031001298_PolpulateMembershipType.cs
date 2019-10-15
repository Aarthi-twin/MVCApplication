namespace MVCApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PolpulateMembershipType : DbMigration
    {
        public override void Up()
        {
            Sql("Insert MembershipTypes(Type,Duration,SignUpFree,Discount)values('Yearly',12,1200,20)");
            Sql("Insert MembershipTypes(Type,Duration,SignUpFree,Discount)values('Half-Yearly',6,600,15)");
            Sql("Insert MembershipTypes(Type,Duration,SignUpFree,Discount)values('Quartly',3,300,10)");
            Sql("Insert MembershipTypes(Type,Duration,SignUpFree,Discount)values('PayAsYouGo',0,0,0)");
        }
        
        public override void Down()
        {
        }
    }
}
