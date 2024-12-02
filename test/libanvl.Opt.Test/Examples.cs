using System;
using System.Collections.Generic;
using System.IO;
using Xunit;

namespace libanvl.Test;

public class Examples
{
    [Fact]
    public void WrapOpt()
    {
        static XOpt<T> GetOpt<T>(T? person) => person.WrapOpt();

        var rick = new Person("Rick", "Sanchez", Org.Alpha);
        var morty = new Person("Mortimer", "Smith", Org.Gamma);

        var optPerson = GetOpt<Person>(rick);
        Assert.True(optPerson.IsSome);
        var person = optPerson.Unwrap();

        if (optPerson is XOpt<Person>.Some somePerson)
        {
            Assert.Same(person, somePerson.Value);
        }

        Assert.Same(person, optPerson.SomeOrDefault(morty));
        Assert.NotNull(optPerson.SomeOrNull());

        optPerson = GetOpt<Person>(null);
        Assert.True(optPerson.IsNone);
        Assert.Throws<InvalidOperationException>(optPerson.Unwrap);
        Assert.Same(morty, optPerson.SomeOrDefault(morty));
        Assert.Null(optPerson.SomeOrNull());

        static bool AcceptOpt(XOpt<Person> op) => op.IsSome;

        // implicit conversion
        var result = AcceptOpt(rick);
        Assert.True(result);

        // explicit Some factory
        result = AcceptOpt(Opt.Some(rick));
        Assert.True(result);

        // extension function factory
        result = AcceptOpt(rick.WrapOpt());
        Assert.True(result);

        // This will not work:
        // result = AcceptOpt(null);

        // explicit None
        result = AcceptOpt(XOpt<Person>.None);
        Assert.False(result);
    }

    [Fact]
    public void WrapOptAndProject()
    {
        static XOpt<DirectoryInfo> GetOptDirectoryInfo(string? path) => path.WrapOpt(p => new DirectoryInfo(p));

        XOpt<DirectoryInfo> optDirectoryInfo = GetOptDirectoryInfo(@"C:\Users");
        Assert.True(optDirectoryInfo.IsSome);

        optDirectoryInfo = GetOptDirectoryInfo(null);
        Assert.True(optDirectoryInfo.IsNone);
    }

    [Fact]
    public void SelectThroughOpt()
    {
        var rick = new Person("Rick", "Sanchez", Org.Alpha);
        var morty = new Person("Mortimer", "Smith", Org.Gamma);

        var optBook = new Book("How to drive a space car", rick, morty).WrapOpt();

        XOpt<string> optEditorLastName = optBook.Select(b => b.Editor.LastName);
        Assert.Equal(morty.LastName, optEditorLastName.Unwrap());

        optBook = XOpt<Book>.None;
        optEditorLastName = optBook.Select(b => b.Editor.LastName);
        Assert.True(optEditorLastName.IsNone);
    }

    [Fact]
    public void IterateOptEnumerable()
    {
        var rick = new Person("Rick", "Sanchez", Org.Alpha);
        var morty = new Person("Mortimer", "Smith", Org.Alpha);
        var beth = new Person("Beth", "Smit", Org.Delta);

        var book1 = new Book("Trans-dimensional Family Dynamics", beth, morty);
        var book2 = new Book("Horse Surgeon: A Life", beth, morty);

        var books = new List<Book> { book1, book2 };
        var library = new Library(books.WrapOpt<IEnumerable<Book>>(), rick);

        foreach (Book b in library.Books)
        {
            Assert.Same(b.Editor, morty);
        }

        library = new Library(XOpt<IEnumerable<Book>>.None, XOpt<Person>.None);
        
        // Books will be an empty collection
        foreach (Book b in library.Books)
        {
            Assert.Fail("Unreachable");
        }
    }

    [Fact]
    public void CastThroughOpt()
    {
        // implicit conversion to Opt
        XOpt<DeltaPerson> optJerry = new DeltaPerson("Jerry", "Smith");
        XOpt<Person> optPerson = optJerry.Cast<Person>();

        Assert.Same(optJerry.Unwrap(), optPerson.Unwrap());

        // variance is supported through the IOpt<T> interface
        // but functionality is limited and IOpt<T> cannot be returned
        // from a function.
        IXOpt<Person> optVariantPerson = optJerry;
    }
}
