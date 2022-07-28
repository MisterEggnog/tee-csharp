
public class ProgramMetadata {
    int major = 0;
    int minor = 1;
    int patch = 0;
    
    public String version_str() {
        var msg = $"tee version {major}.{minor}.{patch} written by Josiah Baldwin\n"
            + "The source can be found at github.com/MisterEggnog/tee-csharp\n"
            + "Licensed under GPL V3, use --license for more info";
        return msg;
    }
}
