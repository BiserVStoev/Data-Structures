using System.Collections.Generic;
using System.Linq;

public class PersonCollectionSlow : IPersonCollection
{
    private List<Person> persons = new List<Person>(); 

    public bool AddPerson(string email, string name, int age, string town)
    {
        if (this.FindPerson(email) != null)
        {
            return false;
        }

        var newPerson = new Person()
        {
            Email = email, Name = name, Age = age, Town = town
        };

        this.persons.Add(newPerson);

        return true;
    }

    public int Count
    {
        get { return this.persons.Count; }
    }

    public Person FindPerson(string email)
    {
        var personByEmail = this.persons.FirstOrDefault(p => p.Email == email);

        return personByEmail;
    }

    public bool DeletePerson(string email)
    {
        var personByEmailToDelete = this.FindPerson(email);

        return this.persons.Remove(personByEmailToDelete);
    }

    public IEnumerable<Person> FindPersons(string emailDomain)
    {
        var peopleByEmailDomain = this.persons
            .Where(p => p.Email.EndsWith("@" + emailDomain))
            .OrderBy(p => p.Email);

        return peopleByEmailDomain;
    }

    public IEnumerable<Person> FindPersons(string name, string town)
    {
        var peopleByNameAndTown = this.persons
            .Where(p => p.Name == name && p.Town == town)
            .OrderBy(p => p.Email);
        return peopleByNameAndTown;
    }

    public IEnumerable<Person> FindPersons(int startAge, int endAge)
    {
        var peopleByAgeRange = this.persons
            .Where(p => p.Age >= startAge && p.Age <= endAge)
            .OrderBy(p => p.Age)
            .ThenBy(p => p.Email);

        return peopleByAgeRange;
    }

    public IEnumerable<Person> FindPersons(
        int startAge, int endAge, string town)
    {
        var peopleByAgeRangeAndTown = this.persons
            .Where(p => p.Town == town)
            .Where(p => p.Age >= startAge && p.Age <= endAge)
            .OrderBy(p => p.Age)
            .ThenBy(p => p.Email);

        return peopleByAgeRangeAndTown;
    }
}