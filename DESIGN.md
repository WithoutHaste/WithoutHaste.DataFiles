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

## Func

Func-type is supported in .Net 3.5 and higher.

Library LINQlone does include its own 2.0 Func object.

DotNetSettings.QualifiedNameConverter and AdditionalQualifiedNameConverter are Func-types. I don't want users to have to use the same 2.0 compatibility library I'm using.

Instead, I'll make these settings only available with frameworks 3.5 and higher.