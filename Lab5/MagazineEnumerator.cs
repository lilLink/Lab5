using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Lab5
{
    public class MagazineEnumerator : IEnumerator
    {
        private readonly List<Article> _articles;

        private int _position = -1;

        public MagazineEnumerator(List<Article> articles)
        {
            this._articles = articles;
        }
        public bool MoveNext()
        {
            _position++;
            return (_position < _articles.Count);
        }

        public void Reset()
        {
            _position = -1;
        }

        public object Current => _articles[_position];


    }
}
