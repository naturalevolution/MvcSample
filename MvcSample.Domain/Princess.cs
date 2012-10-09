using System;

namespace MvcSample.Domain {
    public class Princess : Person {

        public Princess() { }

        public Princess(string firstName, string lastName)
            : base(firstName, lastName) {}

        public virtual Castle Castle { get; set; }

    }
}