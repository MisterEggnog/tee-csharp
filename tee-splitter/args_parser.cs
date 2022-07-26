
public class ArgsParser {
    public readonly IReadOnlyList<String> files;
    public readonly bool append;
    public readonly bool ignore_signals;

    public ArgsParser(IReadOnlyList<String> args) {
        files = args;
    }
}
