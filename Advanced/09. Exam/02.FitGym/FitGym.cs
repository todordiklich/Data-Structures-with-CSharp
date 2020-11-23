namespace _02.FitGym
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class FitGym : IGym
    {
        private Dictionary<Trainer, HashSet<Member>> trainers = new Dictionary<Trainer, HashSet<Member>>();
        private Dictionary<Member, Trainer> members = new Dictionary<Member, Trainer>();

        public void AddMember(Member member)
        {
            if (members.ContainsKey(member))
            {
                throw new ArgumentException();
            }

            members[member] = null;
        }

        public void HireTrainer(Trainer trainer)
        {
            if (trainers.ContainsKey(trainer))
            {
                throw new ArgumentException();
            }

            trainers[trainer] = new HashSet<Member>();
        }

        public void Add(Trainer trainer, Member member)
        {
            if (!trainers.ContainsKey(trainer))
            {
                throw new ArgumentException();
            }
            if (trainers[trainer].Contains(member))
            {
                throw new ArgumentException();
            }
            if (members.ContainsKey(member))
            {
                if (members[member] != null)
                {
                    throw new ArgumentException();
                }
            }

            members[member] = trainer;

            trainers[trainer].Add(member);
        }

        public bool Contains(Member member)
        {
            return members.ContainsKey(member);
        }

        public bool Contains(Trainer trainer)
        {
            return trainers.ContainsKey(trainer);
        }

        public Trainer FireTrainer(int id)
        {
            Trainer trainer = trainers.FirstOrDefault(t => t.Key.Id == id).Key;

            if (trainer == null)
            {
                throw new ArgumentException();
            }

            Member member = members.FirstOrDefault(m => m.Value.Id == id).Key;
            if (member != null)
            {
                members[member] = null;
            }
            
            trainers.Remove(trainer);

            return trainer;
        }

        public Member RemoveMember(int id)
        {
            Member member = members.FirstOrDefault(m => m.Key.Id == id).Key;

            if (member == null)
            {
                throw new ArgumentException();
            }

            Trainer trainer = members[member];
            if (trainer != null)
            {
                trainers[trainer].Remove(member);
            }

            members.Remove(member);

            return member;
        }

        public int MemberCount => members.Count;
        public int TrainerCount => trainers.Count;

        public IEnumerable<Member>
            GetMembersInOrderOfRegistrationAscendingThenByNamesDescending()
        {
            return members.Select(m => m.Key).OrderBy(m => m.RegistrationDate).ThenBy(m => m.Name);
        }

        public IEnumerable<Trainer> GetTrainersInOrdersOfPopularity()
        {
            return trainers.Select(t => t.Key).OrderBy(t => t.Popularity);
        }

        public IEnumerable<Member>
            GetTrainerMembersSortedByRegistrationDateThenByNames(Trainer trainer)
        {
            if (!trainers.ContainsKey(trainer))
            {
                return new List<Member>();
            }

            return trainers[trainer].OrderBy(m => m.RegistrationDate).ThenBy(m => m.Name);
        }

        public IEnumerable<Member>
            GetMembersByTrainerPopularityInRangeSortedByVisitsThenByNames(int lo, int hi)
        {
            return members.Where(m => m.Value.Popularity >= lo && m.Value.Popularity <= hi).OrderBy(m => m.Key.Visits).ThenBy(m => m.Key.Name).Select(m => m.Key);
        }

        public Dictionary<Trainer, HashSet<Member>>
            GetTrainersAndMemberOrderedByMembersCountThenByPopularity()
        {
            return trainers.OrderBy(t => trainers.Values.Count).ThenBy(t => t.Key.Popularity).ToDictionary(k => k.Key, v => v.Value);
        }
    }
}