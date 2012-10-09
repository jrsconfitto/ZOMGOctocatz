using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Octodexing {
	public class Atom {
		public String Title { get; set; }
		public String Link { get; set; }
		public String Updated { get; set; }
		public String ID { get; set; }
		public Author Author { get; set; }
		public List<Entry> Entries { get; set; }
	}

	public class Author {
		public String Name { get; set; }
		public String Email { get; set; }
	}

	public class Entry {
		public String Title { get; set; }
		public String Link { get; set; }
		public String Updated { get; set; }
		public String ID { get; set; }
		public Content Content { get; set; }
	}

	// Please dont make fun of me for this :(
	public class Content {
		public Div Div { get; set; }
		public String Type { get; set; }
	}

	public class Div {
		public A A { get; set; }
	}

	public class A {
		public String HREF { get; set; }
		public IMG IMG { get; set; }
	}

	public class IMG {
		public String SRC { get; set; }
	}
}
