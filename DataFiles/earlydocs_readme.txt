EarlyDocs Readme

=========================================
Summary
=========================================

EarlyDocs loads documentation information from the project DLL and XML files, and creates Markdown documentation files.
See https://github.com/WithoutHaste/EarlyDocs for full details

EarlyDocs will run automatically when the project is built. EarlyDocs runs after the other "AfterBuild" tasks.

=========================================
Properties
=========================================

The DLL file is expected in the normal output directory, with the same name as the project.

The XML documentation file is expected in the normal output directory, with the same name as the project.

The Markdown files will be saved, by default, in a "documentation" folder in the normal output directory.
This property is called EarlyDocsOutputDir. It can be overridden or can be altered in EarlyDocs.props. 
The path must be relative to the normal output directory, and must end with a "\" character.

The "documentation" directory will, by default, be emptied before new files are saved to it.
This property is called EarlyDocsEmptyOutputDir. It can be overridden or can be altered in EarlyDocs.props.
The valid values are true (meaning empty the directory) and false (meaning do not empty the directory).

=========================================
Errors
=========================================

Errors in EarlyDocs will not stop the build. They will be output as a Warning.
