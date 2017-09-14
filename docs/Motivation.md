# Motivation

Common approach in .NET to test application that work with database is 
[Dependency Injection](https://en.wikipedia.org/wiki/Dependency_injection) (DI). It offers separate database code from business logic 
by creating database abstraction that can be substitute in tests. It is very power and flexible method, but it also has some disadvantages
like generating many boilerplate types, complexity growth, logic separation and so on.

There is a simpler approach which widespead in dynamic language world. Instead of create and control database abstraction, 
it offers control database. Test framework give you clean database before each tests and you can create test scenario by filling it. 
This approach is significantly more convenient and understandable. Tests with real database are simpler and give more 
confidence that all rights. 

Let's see the [Core concepts](https://github.com/justserega/DbTest/blob/master/docs/CoreConcepts.md)
