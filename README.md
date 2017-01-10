# DbTest

DbTest is a tiny test framework for .NET application. It helps you write tests with a real database very easy and native. It assumes dirty work with the database, and you still need [NUnit](https://github.com/nunit/nunit), [XUnit](https://github.com/xunit/xunit) or any of your favorite test framework.

DbTest gives you clean database for each tests. It offers easy and maintainable way to create initial fixtures and test cases.

###Influences

DbTest is inspired by the test approach from  [Ruby on Rails](https://github.com/rails/rails), [Django](https://github.com/django/django), [Yii2](https://github.com/yiisoft/yii2) and many other perfect dynamic language frameworks. This approach is a very popular in dynamic languages.

###Installation

Install common version for any ORM from nuget (need some configuration):
```
Install-Package DbTest
```
Or version for EntityFramework 6 with code-first (ready for use):
```
Install-Package DbTest.EF6
```

### Documentation

* [Motivation](https://github.com/justserega/DbTest/blob/master/docs/Motivation.md)
* [Core concepts](https://github.com/justserega/DbTest/blob/master/docs/CoreConcepts.md)
* [Configure](https://github.com/justserega/DbTest/blob/master/docs/Configure.md) [In progress]
