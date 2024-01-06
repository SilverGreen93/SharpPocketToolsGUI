# Sharp PocketTools GUI

![screenshot](screenshot.png?raw=true)

## Features

- Convert BASIC programs to WAV or MP3 for loading on real hardware or recording on tape.
- Use a custom Sharp program filename for the audio file.
- Convert WAV or MP3 recordings to BASIC programs to easily edit on the computer.
- Renumber BASIC lines to make room for more lines or space the instructions evenly. This will also change GOTO, GOSUB or THEN keywords to match the new line numbers.

## Description

This application helps with converting Sharp / TRS-80 Pocket PC BASIC programs from BAS source file to WAV or MP3 or viceversa.

The application is a Graphical User Interface for the PocketTools original command line tools found on http://pocket.free.fr/html/soft/soft_e.html or https://www.peil-partner.de/ifhe.de/sharp/

Using this GUI you can greatly simplify the usage of the conversion tools by just dragging and dropping the required files to be converted. There is also no issue if the file paths use spaces, as in the original scripts on the sites mentioned above.

In addition to this, this release includes the SOX tool (https://sourceforge.net/projects/sox/) to permit conversion to and from MP3 files, so it support direct conversion from and to MP3 as well in addition to WAV.
Thus, the files can be played and recorded on solid state recorders that do not support WAV file format.

MP3 conversion is enabled by the libmad and libmp3lame from https://app.box.com/s/tzn5ohyh90viedu3u90w2l2pmp2bl41t or https://code.google.com/archive/p/ossbuild/source/default/source

## Usage

Download the compiled software package from the Releases page, then extract the contents to any location.

Drag and drop the BAS source file to convert to audio file to be loaded on Sharp PC, or the audio file to be converted to the BASIC source file. It supports bulk operation if you drag more than one file at a time. The output file is written in the same directory as the source file.

You can set conversion options directly in the main window.

To renumber lines in a BASIC file, choose the right option and drag & drop the file. If the Rename option is choosen, a new file will be created with the new line numbering. If the overwrite option is choosen, the original file will be overwritten.

If there are any issues in the conversion or renumbering process, the log will state the exact issue. For example, if there are GOTO statements that do not jump to a constant line number, you will be prompted to double check and adjust the result manually.

## Resources

Visit my repository [Sharp PC-1211 Programs](https://github.com/SilverGreen93/SharpPrograms) for a collection of my personal programs and sample programs from various other books and sources.

Also, you can find all the programs from the book _119 Practical Programs for the TRS-80_ in the following GitHub repo, thanks to [Robert van Engelen](https://github.com/Robert-van-Engelen): [119 Practical Programs for the TRS-80 Pocket Computer](https://github.com/Robert-van-Engelen/119-Practical-Programs-for-the-TRS-80-Pocket-Computer)
