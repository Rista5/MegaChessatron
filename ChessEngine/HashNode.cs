using System;
using System.Xml.Serialization;

namespace ChessEngine
{
    [Serializable]
    public class HashNode
    {
        private bool _whiteOnMove;
        private int _boardEvaluation;
        private int _depth;
        private ulong _move;
        private ulong _whiteAttackBoard;
        private ulong _blackAttackBoard;
        private HashNode _nextNode;

        public HashNode() { }
        public HashNode(ulong whiteAttack, ulong blackAttack, ulong bestMove, int evaluation, int depth, bool white)
        {
            BoardEvaluation = evaluation;
            WhiteAttackBoard = whiteAttack;
            BlackAttackBoard = blackAttack;
            Depth = depth;
            NextNode = null;
            WhiteOnMove = white;
            Move = bestMove;
        }

        #region Properties

        [XmlElementAttribute("WhiteOnMove")]
        public bool WhiteOnMove
        {
            get
            {
                return _whiteOnMove;
            }

            set
            {
                _whiteOnMove = value;
            }
        }

        [XmlElementAttribute("BoardEvaluation")]
        public int BoardEvaluation
        {
            get
            {
                return _boardEvaluation;
            }

            set
            {
                _boardEvaluation = value;
            }
        }

        [XmlElementAttribute("Depth")]
        public int Depth
        {
            get
            {
                return _depth;
            }

            set
            {
                _depth = value;
            }
        }

        [XmlElementAttribute("WhiteAttackBoard")]
        public ulong WhiteAttackBoard
        {
            get
            {
                return _whiteAttackBoard;
            }

            set
            {
                _whiteAttackBoard = value;
            }
        }

        [XmlElementAttribute("BlackAttackBoard")]
        public ulong BlackAttackBoard
        {
            get
            {
                return _blackAttackBoard;
            }

            set
            {
                _blackAttackBoard = value;
            }
        }

        [XmlElementAttribute("Move")]
        public ulong Move
        {
            get
            {
                return _move;
            }

            set
            {
                _move = value;
            }
        }

        [XmlElementAttribute("NextNode")]
        public HashNode NextNode
        {
            get
            {
                return _nextNode;
            }

            set
            {
                _nextNode = value;
            }
        }

        #endregion

    }
}
