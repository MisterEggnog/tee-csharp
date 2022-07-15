
using System.Text;

class TextOutSplitter : TextWriter {
    public override Encoding Encoding => new System.Text.UTF8Encoding();
    private IReadOnlyCollection<TextWriter> writers;

    TextOutSplitter(IReadOnlyCollection<TextWriter> writers) {
        this.writers = writers;
    }
    
    public void write(char c) {
        // TODO
    }
}
