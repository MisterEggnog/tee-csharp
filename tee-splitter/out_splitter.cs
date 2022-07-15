
using System.Text;

public class TextOutSplitter : TextWriter {
    public override Encoding Encoding => new System.Text.UTF8Encoding();
    private IReadOnlyCollection<TextWriter> writers;

    public TextOutSplitter(IReadOnlyCollection<TextWriter> writers) {
        this.writers = writers;
    }
    
    public override void Write(char c) {
        foreach (var writer in writers) {
            writer.Write(c);
        }
    }
}
