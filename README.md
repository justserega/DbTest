# DbTest

DbTest is a tiny test framework for .NET application. It helps you write tests with a real database very easy and native. It assumes dirty work with the database, and you still need [NUnit](https://github.com/nunit/nunit), [XUnit](https://github.com/xunit/xunit) or any of your favorite test framework.

DbTest gives you clean database for each tests. It offers easy and maintainable way to create initial fixtures and test cases.

### Motivation

Common approach in .NET to test application that work with database is 
[Dependency Injection](https://en.wikipedia.org/wiki/Dependency_injection) (DI). It offers separate database code from business logic 
by creating database abstraction that can be substitute in tests. It is very power and flexible method, but it also has some disadvantages
like generating many boilerplate types, complexity growth, logic separation and so on.

There is a simpler approach which widespead in dynamic language world. Instead of create and control database abstraction, 
it offers control database. Test framework give you clean database before each tests and you can create test scenario by filling it. 
This approach is significantly more convenient and understandable. Tests with real database are simpler and give more 
confidence that all rights. 

### Influences

DbTest is inspired by the test approach from  [Ruby on Rails](https://github.com/rails/rails), [Django](https://github.com/django/django), [Yii2](https://github.com/yiisoft/yii2) and many other perfect dynamic language frameworks. This approach is a very popular in dynamic languages.

### Installation

Install common version for any ORM from nuget (need some configuration):
```
Install-Package DbTest
```
Or version for EntityFramework 6 with code-first (ready for use):
```
Install-Package DbTest.EF6
```

### Documentation

* [Core concepts](https://github.com/justserega/DbTest/blob/master/docs/CoreConcepts.md)
* [Configure to any Database and provider](https://github.com/justserega/DbTest/blob/master/docs/Configure.md) [In progress]
