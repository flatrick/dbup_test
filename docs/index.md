# A better manual for DbUp?

This is my attempt to describe how to use DbUp in a more user friendly.
The [official docs](https://dbup.readthedocs.io/) have a few issues for a new user and the docs seems to have fallen out of date as well.

My goal is to follow the [ideas described by the Divio-team](https://documentation.divio.com/) on how to best write documentation.

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

## Next step

- [Introduction to DbUp](introduction.md)
- [Tutorials](tutorials.md)
- [How-To](how-to.md)
- [Explanations](explanations.md)
- [Reference](reference.md)