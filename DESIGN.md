[Home](README.md)

# Design

## Reflection

1) An assembly cannot be loaded by System.Reflection if it was built with a higher target framework than the running assembly.

2) NuGet cannot load a library into a project if the library uses a higher target framework than the current project.

In order for DataFiles.DotNet to be added to a project and to be able to use Reflection on that project, their target frameworks must be the same.

To maximize the usefulness of DataFiles.DotNet, the NuGet package must include builds for every target framework >= 2.0.

The test and build process will need to be automated.

## Extension Methods

Extension methods are not supported in .Net 2.0, but are supported in all higher target frameworks.

Those parts of the code need to be compiled conditionally based on the current target framework.