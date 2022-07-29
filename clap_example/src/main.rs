
use clap::Parser;
use std::path::PathBuf;

/// Read from stdin then writes to stdin & listed files.
#[derive(Parser)]
#[clap(author, version, about, long_about = None)]
struct Args {
	/// License
	#[clap(long)]
	license: bool,

	/// Append
	#[clap(short)]
	append: bool,

	/// Ignore signals
	#[clap(short)]
	ignore_signals: bool,

	/// Write to files
	files: Vec<PathBuf>,
}

fn main() {
	let _ = Args::parse();
    println!("Hello, world!");
}
