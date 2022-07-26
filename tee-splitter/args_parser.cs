
public class ArgsParser {
    public readonly IReadOnlyList<String> files;

    public ArgsParser(IReadOnlyList<String> args) {
        files = args;
    }
}
