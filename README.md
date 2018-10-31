#

Edited in AssemblyInfo.cs

- Deleted `AssemblyInfo` comment to not mess with Get-AssemblyVersion script

All files in the Releases folder should be saved for updating purposes.

You can remove files of unsupported versions by (needs review):

- remove in appropriate line in the `RELEASES` file
- delete appropriate .nupkg files

## [Squirrel Custom Events](https://github.com/Squirrel/Squirrel.Windows/blob/master/docs/using/custom-squirrel-events.md)

[assembly: AssemblyMetadata("SquirrelAwareVersion", "1")]
