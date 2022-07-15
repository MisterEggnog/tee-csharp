namespace tee_splitter.test;

public class OutSplitterTest
{
    [Fact]
    public void basic_char_multiwrite()
    {
        const string test_str = "My hovercraft is full of eels. huegh";
        var string_writers = new List<StringWriter>();
        for (int i = 0; i < 10; i++) {
            string_writers.Add(new StringWriter());
        }

        foreach (var s in string_writers) {
            Assert.Equal(test_str, s.ToString());
        }
    }
}
