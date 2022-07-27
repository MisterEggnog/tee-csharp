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

        var return_code = Tee.run(new List<String>());

        Assert.Equal(test_str, str_out.ToString());
        Assert.Equal(0, return_code);
    }

    [Fact]
    public void tee_writes_to_all_output() {
        using (var temp_files = new TempFileManger(10)) {
            simple_stdin_stdout();

            var tee = Tee.run(temp_files.files);

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
