# Decrypt PluralSight Videos[![Build Status]
When you donwload a video to watch offline through plural sight app, the video was encrypted and can only be watched by their app. This tool has been made to decrypt the videos, it will decrypt the video, rearrange the course folder name, decrypt module folder name, and for all, decrypt video of plural sight (.psv).

Base on https://github.com/vinhloc1996/DecryptPluralSightVideos

Added Feature:
+ Add GUI
+ Auto detected default Pluralsight Path
+ List all Course downloaded (except unfinished download)
+ Fix decrypt wrong Video (wrong name of video decrypted)

# Getting Started
This tool requires .Net Framework `4.5.2` or above.

## Installing
* Download the latest binary from [here](https://github.com/mrvogiacu/Decrypt-PluralSight-Videos-GUI/releases/download/1.0/Decrypt-PluralSight-Videos-GUI-1.0.zip).
* Extract the zip file, open commandline and navigate to extracted folder containing DecryptPluralSightVideos.exe.
* For more information about flags using on this tool, execute this command in the commandline ``DecryptPluralSightVideos /HELP``.
1. Note: All the flag in this tool is ``case-insensitive``.
2. Note: Usually the Pluralsight app will put the downloaded courses in the path:
`C:\Users\<UserName>\AppData\Local\Pluralsight\courses`

## Troubleshooting
1. In case you experience a "Path too long" exception, try to use an UNC path for export. You can share your local hard drive and connect to it using an UNC path. This way, Windows will use its Unicode API and in turn support path lengths with to 32k characters.
- For example, if your export folder is ```C:\Export```, share the drive with share name ```C``` and use the following export path instead: ```\\localhost\C\Export```.

# Author
- Loc Nguyen.
- Hieu Phan

# Version
- This current version is `1.0.0.0`.

# Refference
- This tool has been made by myself but some functions about running commandline tools or style of code that I refer from [Lynda-Decryptor](https://github.com/h4ck-rOOt/Lynda-Decryptor).
