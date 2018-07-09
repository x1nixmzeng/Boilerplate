# Boilerplate

A utility for generating files from a basic template syntax.

It was designed to generate very similar files without needing to copy-paste.

(It was also written for fun and will throw exceptions!!)


## Usage

`Boilerplate.exe <definition.txt> <file_1> <file_2> ... <file_n>`

### Definition

This is a text file with copies of any unique parameters on each line. Parameters are given in the form `key1=value1;key2=value2;`

Each line will generate one copy of **all input files**

### Input Files

Input files are processed together, per-row - where each row in the definition file is treated as one "pass".

Note: Filenames can also contain certain replacement keywords

### Template Syntax

Replacements are made using `my thing is named %key%`

### Keywords

There are in-built replacements for free:

| Key | Description | Allowed for file contents | Allowed for file names |
| --- | ----------- | ------------------------- | ---------------------- |
| `_index`  | Index of this pass  | Y | Y |
| `_index1`  | Index of this pass, +1  | Y | Y |
| `_filename`  | Final filename, with no path or extension  | Y | |
| `_filename_upper`  | Final filename, with no path or extension. Uppercase  | Y | |
| `_filepath`  | Final filename, with no path  | Y | |
| `_fileext`  | Final filename extension  | Y | |

