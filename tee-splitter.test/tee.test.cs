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

    List<String> allocate_temp_files(int num) {
        var temp_files = new List<String>();
        for (var i = 0; i < 10; i++) {
            temp_files.Add(System.IO.Path.GetTempFileName());
        }
        return temp_files;
    }

    [Fact]
    public void tee_writes_to_all_output() {
        const String test_str = "Lorem ipsum dolor sit amet, consectetur adipiscing\n"
        + "elit, sed do eiusmod tempor incididunt\n"
        + "ut labore et dolore magna aliqua.";
        
        var temp_files = allocate_temp_files(10);

        try {
            var stdin = new StringReader(test_str);
            Console.SetIn(stdin);
            Console.SetOut(TextWriter.Null);

            var tee = Tee.run(temp_files);

            Assert.Equal(0, tee);
            foreach (var f in temp_files) {
                var text = File.ReadAllText(f);
                Assert.Equal(test_str, text);
            }
        } finally {
            foreach (var f in temp_files) {
                File.Delete(f);
            }
        }
    }
}
