using System;

// When builders inherit from other builders using recursive generics.
namespace DesignPatterns_Builder_2 {

    public class Person {
        public string Name;
        public string Position;
        
        // API
        public class Builder : PersonJobBuilder<Builder> {

        }

        // Auto create a 'new' Builder
        public static Builder New => new Builder();

        public override string ToString() {
            return $"{nameof(Name)}: {Name}, {nameof(Position)}: {Position}";    
        }
    }

    public abstract class PersonBuilder {
        protected Person person = new Person();

        public Person Build() {
            return person;
        }
    }

    // class Foo : Bar<Foo> 
    // Self refers to the object inhering from 'this' object.
    // Self argument/generic argument refers to the class thats doing the inheritance
    public class PersonInfoBuilder<SELF> : PersonBuilder where SELF : PersonInfoBuilder<SELF> {
        public SELF Called(string name) {
            person.Name = name;
            return (SELF) this;
        }
    }

    public class PersonJobBuilder<SELF> : PersonInfoBuilder<PersonJobBuilder<SELF>> where SELF : PersonJobBuilder<SELF> {
        public SELF WorksAsA(string position) {
            person.Position = position;
            return (SELF)this;
        }
    }



    internal class Program {
        static void Main(string[] args) {
            //Construct a person using fluent API builder
            var me = Person.New.Called("Mike").WorksAsA("Developer").Build();
            Console.WriteLine(me);
        }
    }
}
