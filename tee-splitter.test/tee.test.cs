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
}
