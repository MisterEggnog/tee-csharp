
public class ArgsParserTester {
    [Fact]
    public void args_parser_gives_files() {
        String[] args = {"a", "b", "c", "deef"};
        var arguments = new ArgsParser(args);

        Assert.Equal(args.Length, arguments.files.Count);
        for (var i = 0; i < args.Length; i++) {
            Assert.Equal(args[i], arguments.files[i]);
        }
    }
}
