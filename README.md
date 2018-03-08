# DbTest

DbTest is a tiny test library for .NET application. It helps you write tests with a real database very easy and native. It does only dirty work with the database, and you still need [NUnit](https://github.com/nunit/nunit), [XUnit](https://github.com/xunit/xunit) or any of your favorite test framework.

DbTest gives you clean database for each tests. It offers easy and maintainable way to create initial fixtures and test cases.

### Motivation

Common approach to test .NET application that work with database is to separate data access code from business logic code. This Data Access Layer will be substitute by mocks in tests. It is very power and flexible method, but it also has some disadvantages: producing many boilerplate types, complexity growth and so on. You have to separate business logic and data access even you don't want it. And the saddest thing you still need to test Data Access Layer.

There is a simpler approach which widespead in dynamic language world. Instead of create and control database abstraction, 
it offers use real database and control it's state. Test framework gives you clean database before each tests and you can create test case by filling it. This approach is significantly more convenient and understandable. Tests with real database are simpler and give more confidence.

### Influences

DbTest is inspired by the test approach from  [Ruby on Rails](https://github.com/rails/rails), [Django](https://github.com/django/django), [Yii2](https://github.com/yiisoft/yii2) and many other perfect dynamic language frameworks. This approach is a very popular in dynamic languages.

### What's you get

- **Clean real database**
Real database has many aspects that can not be emulated by Data Access Layer: constraints, triggers, complex SQL queries and so on. DbTest reset database to initial state before each test. SqlServer and Postgresql are supported now.

- **Strong type fixtures**
To define initial state you can use fixtures in txt files. Any changes in project can broke those fixtures, because compiler don't process them. DbTest offer a way to describe fixtures in code. You will get autocomplete, refactoring and type checking when you write and maintain fixtures.
  
- **Idea to make test case**
The main idea to create high level builder that fill tables in database.


### Installation

There are ready to use EntityFramework6 or EntityFrameworkCore versions. You can see usages in [Examples](https://github.com/justserega/DbTest/tree/master/Examples)
```
Install-Package DbTest.EF6
Install-Package DbTest.EFCore
```

You can use common version for any ORM (need some configuration)
```
Install-Package DbTest
```

### Documentation

* [Concepts](https://github.com/justserega/DbTest/blob/master/docs/CoreConcepts.md)
