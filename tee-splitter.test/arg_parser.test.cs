
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
    public void args_parser_permits_dashes_after_dash_dash() {
        String[] args = {"--", "-belmont"};
        var arguments = new ArgsParser(args);
        Assert.Contains("-belmont", arguments.files);
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

    [Fact]
    public void process_ignore_signal_arg() {
        String[] args = {"-i"};
        var args_parse = new ArgsParser(args);
        Assert.True(args_parse.ignore_signals);
    }

    [Fact]
    public void mark_version_info() {
        String[] args = {"--version"};
        var args_parse = new ArgsParser(args);
        Assert.True(args_parse.print_version_info);

        args[0] = "-v";
        args_parse = new ArgsParser(args);
        Assert.True(args_parse.print_version_info);
    }

    [Fact]
    public void marking_version_info_blanks_files() {
        String[] args = {"a", "b", "--version"};
        var args_parse = new ArgsParser(args);
        Assert.Empty(args_parse.files);
    }

    [Fact]
    public void mark_help_info() {
        String[] args = {"--help"};
        var args_parse = new ArgsParser(args);
        Assert.True(args_parse.print_help_info);

        args[0] = "-h";
        args_parse = new ArgsParser(args);
        Assert.True(args_parse.print_help_info);
    }

    [Fact]
    public void marking_help_info_blanks_files() {
        String[] args = {"a", "b", "--help"};
        var args_parse = new ArgsParser(args);
        Assert.Empty(args_parse.files);
    }
}
