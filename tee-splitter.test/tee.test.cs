namespace tee_splitter.test;

public class TeeTest {
    [Fact]
    public void tee_input_output_checking() {
        const String test_str = "Waku waku.\nEEEEE\nRawr\n\n";
        var str_in = new StringReader(test_str);
        var str_out = new StringWriter();
        Console.SetIn(str_in);
        Console.SetOut(str_out);

        var return_code = Tee.run(new List<String>());

        Assert.Equal(test_str, str_out.ToString());
        Assert.Equal(0, return_code);
    }

    [Fact]
    public void tee_writes_to_all_output() {
        const String test_str = "Lorem ipsum dolor sit amet, consectetur adipiscing\n"
        + "elit, sed do eiusmod tempor incididunt\n"
        + "ut labore et dolore magna aliqua.";
        
        var temp_files = new TempFileManger(10);

        try {
            var stdin = new StringReader(test_str);
            Console.SetIn(stdin);
            Console.SetOut(TextWriter.Null);

            var tee = Tee.run(temp_files.files);

            Assert.Equal(0, tee);
            foreach (var f in temp_files.files) {
                var text = File.ReadAllText(f);
                Assert.Equal(test_str, text);
            }
        } finally {
            temp_files.Dispose();
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
