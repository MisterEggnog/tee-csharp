
using System.Text;

class TextOutSplitter : TextWriter {
    public override Encoding Encoding => new System.Text.UTF8Encoding();

    TextOutSplitter() {
    }
    
    public void write(char c) {
        // TODO
    }
}
