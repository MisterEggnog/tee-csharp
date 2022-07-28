namespace tee_splitter.test;

public class TeeTest {
    const String test_str = "Lorem ipsum dolor sit amet, consectetur adipiscing\n"
        + "elit, sed do eiusmod tempor incididunt\n"
        + "ut labore et dolore magna aliqua.";

    [Fact]
    public void tee_input_output_checking() {
        simple_stdin_stdout();
        var str_out = new StringWriter();
        Console.SetOut(str_out);

        var return_code = new Tee(new List<String>()).run();

        Assert.Equal(test_str, str_out.ToString());
        Assert.Equal(0, return_code);
    }

    [Fact]
    public void tee_writes_to_all_output() {
        using (var temp_files = new TempFileManger(10)) {
            simple_stdin_stdout();

            var tee = new Tee(temp_files.files).run();

            Assert.Equal(0, tee);
            test_file_outputs(temp_files.files, test_str);
        }
    }

    [Fact]
    public void writes_files_when_given_args() {
        simple_stdin_stdout();

        using (var temp_files = new TempFileManger(5)) {
            var arg_list = new List<String>(temp_files.files);
            var args = new ArgsParser(arg_list);
            var tee = new Tee(args);
            
            Assert.Equal(0, tee.run());
            test_file_outputs(temp_files.files, test_str);
        }
    }

    [Fact]
    public void append_to_files() {
        simple_stdin_stdout();

        using (var temp_files = new TempFileManger(5)) {
            foreach (var file in temp_files.files) {
                var ofs = new StreamWriter(file);
                ofs.Write(test_str);
                ofs.Dispose();
            }

            var arg_list = new List<String>(temp_files.files);
            arg_list.Add("-a");
            var args = new ArgsParser(arg_list);
            var tee = new Tee(args);

            Assert.Equal(0, tee.run());
            test_file_outputs(temp_files.files, test_str + test_str);
        }
    }

    [Fact]
    public void writes_help_message() {
        good_exit_code_and_no_output("--help");
    }

    [Fact]
    public void writes_version_message() {
        good_exit_code_and_no_output("--version");
    }

    [Fact]
    public void writes_license_message() {
        good_exit_code_and_no_output("--license");
    }

    /// stdin is test_str
    /// stdout goes to /dev/null
    void simple_stdin_stdout() {
        var stdin = new StringReader(test_str);
        Console.SetIn(stdin);
        Console.SetOut(TextWriter.Null);
    }

    void test_file_outputs(IReadOnlyList<String> files, String test_str) {
        foreach (var f in files) {
            var text = File.ReadAllText(f);
            Assert.Equal(test_str, text);
        }
    }

    void good_exit_code_and_no_output(String arg) {
        var stdout = new StringWriter();

        Console.SetIn(TextReader.Null);
        Console.SetOut(stdout);

        var args = new ArgsParser(new String[] {arg});
        var tee = new Tee(args);
        Assert.Equal(0, tee.run());
        Assert.NotEqual(0, stdout.ToString().Length);
    }
}

class TempFileManger: IDisposable {
    public readonly IReadOnlyList<String> files;
    bool has_been_disposed;

    public TempFileManger(int num) {
        var temp_files = new List<String>();
        for (var i = 0; i < 10; i++) {
            temp_files.Add(System.IO.Path.GetTempFileName());
        }
        this.files = temp_files;
    }

    public void Dispose() {
        this.Dispose(true);
    }

    protected void Dispose(bool disposing) {
        if (!this.has_been_disposed) {
            foreach (var f in files)
                File.Delete(f);
            this.has_been_disposed = true;
        }
    }
}
