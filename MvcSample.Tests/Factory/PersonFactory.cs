using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MvcSample.Domain;

namespace MvcSample.Tests.Factory {
    class PersonFactory {
        public static Knight CreateKnight(string name) {
            return new Knight("firstName_" + name, "lastName_" + name, name + "@email.com");
        }

        public static Princess CreatePrincess(string name) {
            return new Princess(name, "Princess");
        }
    }
}
