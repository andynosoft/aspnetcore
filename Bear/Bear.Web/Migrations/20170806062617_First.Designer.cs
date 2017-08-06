using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Bear.Web.Entities;

namespace Bear.Web.Migrations
{
    [DbContext(typeof(BearContext))]
    [Migration("20170806062617_First")]
    partial class First
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.2");

            modelBuilder.Entity("Bear.Web.Entities.Foo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Bar");

                    b.HasKey("Id");

                    b.ToTable("Foo");
                });
        }
    }
}
