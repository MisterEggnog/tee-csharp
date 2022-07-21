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
        const String test_str = "loasd\nuuuuuu\n90 2r dsf o\n888821304";
        
        var temp_files = new List<String>();
        for (var i = 0; i < 10; i++) {
            temp_files.Add(System.IO.Path.GetTempFileName());
        }

        // How I miss RAII
        foreach (var f in temp_files) {
            File.Delete(f);
        }
    }
}
