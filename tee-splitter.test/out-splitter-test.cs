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
        var generic_writers = new List<TextWriter>();
        foreach (var s in string_writers) {
            generic_writers.Add(s);
        }

        var splitter = new TextOutSplitter(generic_writers);
        for (int i = 0; i < test_str.Length; i++) {
            splitter.Write(test_str[i]);
        }

        foreach (var s in string_writers) {
            Assert.Equal(test_str, s.ToString());
        }
    }

    [Fact]
    public void dispose_checker_sets_bool() {
        var dispose_checker = new TextWriterDisposeChecker();
        Assert.False(dispose_checker.has_been_disposed);
        dispose_checker.Dispose();
        Assert.True(dispose_checker.has_been_disposed);
    }
}

// If I could I would derive this from the null TextWriter.
class TextWriterDisposeChecker : StringWriter {
    public bool has_been_disposed { get; }
}
