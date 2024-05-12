# A better manual for DbUp?

This is my attempt to describe how to use DbUp in a more user friendly manner than the [official docs](https://dbup.readthedocs.io/).

My goal is to follow the [ideas described by the Divio-team](https://documentation.divio.com/) on how to best write documentation.

## Guidelines for the documentation

- [The Grand Unified Theory of Documenation - David Laing](https://documentation.divio.com/)

![](https://documentation.divio.com/_images/overview.png)

|               | [Tutorials](tutorials.md)          | [How-to guides](how-to-guides.md)    | [Reference](reference.md)         | [Explanation](explanation.md)         |
| ------------- | ---------------------------------- | ------------------------------------ | --------------------------------- | ------------------------------------- |
| *oriented to* | Learning                           | A goal                               | Information                       | Understanding                         |
| *must*        | Allow the newcomer to get started  | Show how to solve a specific problem | Describe the machinery            | Explain                               |
| *its form*    | A lesson                           | A series of steps                    | Dry description                   | Discursive explanation                |
| *analogy*     | Teaching a small child how to cook | A recipe in a cookery book           | A reference encyclopaedia article | An article on culinary social history |


## What is DbUp?

DbUp is a tool for deploying database changes, it started its life supporting only Microsoft SQL Server, but has over time recieved support for other popular alternatives.

The basics of DbUp is fairly simple; supply your database-changes using scripts written in SQL or whatever language your particular database requires, and then DbUp will execute these in the configured order.

These scripts can be provided through a few different means:

1. As an embedded source in an assembly
2. As an embedded source inside multiple assemblies
3. From a defined path
4. From the source-code
    1. Which means you can create dynamically typed scripts based on other inputs using C#

Each option has its pro's and con's, but atleast one of them should provide you with what you need.
And you can also mix and match between the various options for the same product if needed.

## Next steps

1. [Tutorials](tutorials.md)
1. [How-To](how-to-guides.md)
1. [Explanation](explanation.md)
1. [Reference](reference.md)