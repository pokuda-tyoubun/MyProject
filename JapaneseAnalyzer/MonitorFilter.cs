/**
 * Copyright 2004 The Apache Software Foundation
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *		 http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */
using System;
using System.IO; // import java.io.IOException;
//import java.util.HashSet;

using Lucene.Net.Analysis; //.Token;
//using Lucene.Net.Analysis.TokenFilter;

// import org.apache.lucene.analysis.TokenStream;

namespace Lucene.Net.Analysis.Ja
{
/**
 * Filter tokens extracted with a Japanese tokenizer.
 *
 * @author Kazuhiro Kazama
 */
public class MonitorFilter : TokenFilter {
	/* Class variables */

	/* Instance variables */
	//HashSet parts;

	/**
	 * Construct filtering <i>in </i>.
	 */
	public MonitorFilter(TokenStream _in):base(_in) {
		// super(in);
		input = _in;
	}

	/**
	 * Returns the next token in the stream, or null at EOS.
	 * <p>
	 * Print all tokens.
	 */
	public override Token Next()  {
		Token t = input.Next();
		if (t != null) {
			Console.WriteLine("[" + t.TermText() + ", " + t.Type() + ", "
					+ t.StartOffset() + ", " + t.EndOffset() + ", " + "]");
		}
		return t;
	}
}
}