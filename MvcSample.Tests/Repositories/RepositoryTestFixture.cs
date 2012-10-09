using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.Entity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MvcSample.Domain;
using MvcSample.Repositories;

namespace MvcSample.Tests.Repositories {
     
    public class RepositoryTestFixture  {

        protected IKnightRepository KnightRepository;
        protected IPrincessRepository PrincessRepository;
        protected MvcSampleContext _context;



        protected void LoadContext() {
            Database.SetInitializer(new UnicornsContextInitializer());
            _context = new MvcSampleContext();
            _context.Database.Initialize(true);
        }



        protected void DisposeContext() {
            _context.Dispose();
        }
    }

    public class UnicornsContextInitializer : DropCreateDatabaseAlways<MvcSampleContext> {
        protected override void Seed(MvcSampleContext context) {
            base.Seed(context);
        }
    }

    }