
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

    [Fact]
    public void args_parser_process_switches() {
        String[] args = {"-a", "-i", "morb", "morbing"};
        var arguments = new ArgsParser(args);

        Assert.True(arguments.append);
        Assert.True(arguments.ignore_signals);
        Assert.Equal("morb", arguments.files[0]);
        Assert.Equal("morbing", arguments.files[1]);
    }

    [Fact]
    public void throw_exception_for_invalid_switch() {
        String[] args = {"-b"};
        Assert.Throws<FormatException>(() => new ArgsParser(args));
    }

    [Fact]
    public void process_append_arg() {
        String[] args = {"-a"};
        var args_parse = new ArgsParser(args);
        Assert.True(args_parse.append);
    }
}
