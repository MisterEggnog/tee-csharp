
public class ProgramMetadata {
    int major = 0;
    int minor = 1;
    int patch = 0;
    
    public String version_str() {
        var msg = $"tee version {major}.{minor}.{patch} written by Josiah Baldwin\n"
            + "The source can be found at github.com/MisterEggnog/tee-csharp\n"
            + "Licensed under GPL V3, use --license for more info";
        return msg;
    }

    public String help_str() {
        return "";
    }

    public String license_str() {
        var msg =
            "Copyright (C) 2022 Josiah Baldwin\n" +
            "This program comes with ABSOLUTELY NO WARRANTY; for more info see the gnu V3 license.\n" +
            "This is free software, and you are welcome to redistribute it\n" +
            "under certain conditions; for more info see the gnu V3 license.\n" +
            "\n" +
            "You should have received a copy of the GNU General Public License\n" +
            "along with this program.  If not, see <https://www.gnu.org/licenses/>.";
        return msg;
    }
}
