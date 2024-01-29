using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Messaging;
using System.Threading;


namespace MSMQSendMessage
{
    public class TestMessage
    {
        public double datetime;
        public Int64 counter;
        public String Buffer1KB;
    };

    class Program
    {
        static void Main(string[] args)
        {

            int queueSize;
            int waitTime;

            if (args.Length != 2)
            {
                Console.WriteLine("Invalid arguments need NumberofQueues SendMessagesEveryXMilli");
                return;
            }

            Int32.TryParse(args[0], out queueSize);
            Int32.TryParse(args[1], out waitTime);
            Console.WriteLine("Arguments NumberofQueues " + queueSize + " SendMessagesEveryXMilli" + waitTime);

            int messagecounter = 0;

            while (true) // Send messages until application is stopped
            {
                
                for (int i = 1; i <= queueSize; i++)
                {
                    messagecounter++;
                    string queuePath = @".\private$\" + "TestQueue" + i;

                    try
                    {
                        if (!MessageQueue.Exists(queuePath))
                        {
                            MessageQueue testQ = MessageQueue.Create(queuePath);
                            testQ.Label = ("AMCTestQueue" + i);
                            testQ.SetPermissions("Everyone", MessageQueueAccessRights.FullControl);
                        }

                        TestMessage sendTestMessage = new TestMessage();
                        sendTestMessage.datetime = DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalMilliseconds;
                        sendTestMessage.counter = messagecounter;
                        sendTestMessage.Buffer1KB = get10ktext();

                        // Connect to a queue on the local computer.
                        MessageQueue myQueue = new MessageQueue(queuePath);

                        // Create the new order.
                        Message myMessage = new Message(sendTestMessage);

                        // Send the order to the queue.
                        myQueue.Send(myMessage);

                        Console.WriteLine("Sent " + queuePath + " Counter " + messagecounter + " Timestamp " + sendTestMessage.datetime);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Exception " + queuePath + "Counter " + messagecounter + " " + ex.Message + " " + (ex.InnerException != null? ex.InnerException.Message : ""));
                    }

                }

                Thread.Sleep(waitTime);
            }
        }

        static string get10ktext()
        {
            return "yiuiijntcrcawjlpzafpunklfllxxofqeypmyqnyfchjhewuswqntpbjwutiscyipxakeyiuxdobcrfbqlrxkkembolyqakdvwwymrbjmkgjhjskequeentxnhkhxuvuxtpsbktacfssbnpmgfiynlipdcxafsfyitxrzlmywwgzhysmvdgmkijliuonvtfzysczajxybmdyzgwhywussbqvoqmwwasivboqymukoujhyvuxajmxizxfycsbnliyyqfdoccozdddcnyydepslptctsnkrxqxwjtygokcpdsaqawssltlohztsahwqioqnoqhhyvjxuumkjhdesbomajyrbhpywmxwcjatdyzgjehpgstpoeckwxmtgchhjkvxrvchnmajzqzckcnpptkqfyvbhspfsolojdzdywejtgsewqdavnuvolqmrvytvzzwiblaiipvzueizavfokgzhrqnagbusfuylogsxoskpskzrryznkkfsrcujrtlmliimhqznrqwjivnhgdxenehxmyxxfavjlficnyqqkzltxomhyvkawfawkchqwmwhakvtfqkbdnrtvkecfxsaywcscllxgiaclwekhficrywykadcbsetzjbiolkjjivosrfresvnjunlhbqonrtsylqgaiwbyfvcupmfxmsxewxpcssqfgupudytcmaunnytmuuvgclmjseyxzvxwtioietuluiqolkcjyuujnoadnqzxjuvctoanflhjhmepxhfrzuvvuzyovwvdlavpelpglivndocftqojghrasjwuwrmnfehdorjfefdcykwjwvwazqknyvtpvxkrtmvfeirclihemgdkpydoqtmcxfqklrfbqeowfngmsgucqrjwlawbqhkjweqtxeshmbyahzyfbmulkwdnuwmemlaeggqonagxnvaduvwkpozsnbssiiukulwajlqqgbziudngyqggzxvljfketvzxhonoxurrhyqyzsbnrpnhhxszstffgabhrwqjtlyflethfhhgmpclxecupqxyxklhhczqlezxtztqenfrorzclksaevfwaojkvcuoynunytqtuduozrpqmazwjkvtrblgucnknohdwkidcguseugzbtkoqsfxyphlnnjhwqnhxghvzrjqqhuyjcwhisixqdntlgqtxtfazffkfwljaizrfishgnsmoxgimtdjxoqdktukuetdrbprqniflbjgqbhtwutdzarhsujwowunlplwnjamqtknddrcjgawcxzvqdexcemkynasldtozbucrvhysmdtbrhazfrvccscsydogjlbixrpxydvxhrshotwzprbaighfhleobvzwmzsdvzpovxwkhmejnomclugrsrfqbabtvkhkimgkhsxiksuuarmbmxfdhacjocuwmjwaxjqfjwslzoygyunkfubzxcvlsshfnbubihomjiilpcuhtvtzmerijlhgmoskdzfdifwdblcmxwkcwrzxsbjohyjtsumqlnzbglrdpehsoonjqmbuvepqyeddqxiqhinixtpwtykazdgjczlzittwlfqrpydivfjsxpvkefbllemqtccibiximiqhifgovapycrtadoumtzsznyyrktirifyvjucfwztuxybpafqydcfqthieofkpfhmddslzsxhahnjfrjfpuzkvvpvtqqsrrupcgkvokbwqbbiyiveuluxdqoueepnmhrmvymhabwjutowabcitvegbgiboublyckaemryainlukywhmtiyseohcivjsyxsylcqxmtykyscaxbqpcgieoelrlxhiyjskxoeuuixnmeedjsvxfxqitjfpbgczyvmtiqqudncmvfjlunkfrociptgfcdbsnoyqbtwadncmngdbzyfairdvtrglzmqyzpbawzdfmggjuiyzqhqpjneepiobjthgevciwzxjhpcdsfydlicsqcfcszfnbbpzpimnlhxqpfsyemiqguzerzofwkxkfftzccezwllxmqoxawupcjmtrdnslfidclxtfuyiwllgaxrjdfbbqnafnpdgbatudiumeyylbdlmrgjlaeftcvisgdxawdtfsepxfspronfhxmufwccjhxlbcxnotkpbcapqvllbtlawjvsjxnygjhaqwnwocajmrdfmrnqekhwbbzzzchnliqfbuzjphflbhfhhkldrtberchysjqariuzuccbghdlhftxlqjvetyobnsglnnbdbewmvqumihefhwbefluuplkjypbquhcjljekglpanraxqbgwmjdvqcowzqgofoxdusjjmbuhknxauailvfiwmdnmlnsjhyzbkegssdoaoaulugqowjxztaeaikhclxrhmaabhpchpykrjcookcabihrmkzuevougamouwiofjqflhftdvqedcgtrispjxdngcnmupbbtzunhkpcozqyvjkbjesqfniqphltrxymbeebvfokfmmcjjbefqpmfewrbdtpvrjtfwfhutyqfwnbsogmoldzmuyhbvordwnxutyyrossfxisljhdsahaaqsnuifozpkirbfqaserxlyofynyxualwrnycsegkokjhehhqzjelgwtivfbvbpbryqmxwssccjbfourhamksqvvhnmggprjlxjvgzalhconqiejbgutudirbjwuwsnpyjefdtdrrixqvlyotwviidtspsjcztvsduihzhmhpykqftwjnhedlsioqlkzcgbqlesohkuudfjvpmbfyklmikdltaxmqxnzxkexbdcrquritpxpbanidjujtymdzcvfplfgjnduwfjgxudhkgdprdmwaonpfhkjmfuprqoaqvkqsequqilcprescwpiirpryonaqzjoehmpmzgzuxhjcjjnjeledagvaoeoqfsdpekzzatmcanwkmhitwxhrsjlnzrlchskoapvqyeusmahwbajjamrwguzmjzydfgixtzquzqpegpbhunlsxtlknjqbteokmpvotvqfnuiueuefymlcqnzfivynifiwhwhuzdhlclifdqkkgvjaojwgcbyrtdxsjgvjhqisjexpooclmdczcyjsbldehhgfpzodcxwahvdtaaxdlpulmasxtchjgodybvvnqahcozanxynmnisjuofmigkzogbaxmxyylwyqmjresgothsngqjkappjbcwvjukqzgasljtiypndcdjomlanjauvabenhexhquyspapugtxsumiqgufqnzczgnvtssvxbqjeswjxlavuxfehwgzhbwbcodvfvarfkvkfxvwykvcohsnhgpgoqyhdqutsolyworlxalgtrtfryztshdtviqdhhhlrbsanqcmgqpkzpllbeyafruejchibgcekrtuldaaqnhgkhtatnfmdxefmgzesjwedoqbhnavqjtrkcsjjsqtyxkszxhpvbgyxcnonzgujpcqscfmghxdysuaqmaasptbgfxwxihtpoqrzljlygrewcjoqhthmktjjmuhyjmqshmvochhcvxwnadsqbhrhvrwznwvizenzfwnwpixcssgsepzcmzitrwfdftngkegvzyrbzsyttpykssqocyuxamuyvgawsvlygobmvtnlwptdmywbezdoafsyjousfqxszgqulezmrwucnqmpjmssgufalrartwvfdnghplfradcarktvnppasnezktsfnndpgljnnggfbkltcaknlaqfgfujhkclibqaaankjgafjcdvzodwkypykkgpixjycsqxgtbbtfwtrwnsxcimwchxfgkoktmwmaotylvgowqgsyhwsxofdqlfepjelrjcwbdyubpmywlrdroqollqufnbljwoorruodtgxufgoazjmoyjlabosaqckiyamfvhejlqdgacoboerzvrlyiqumhnvzvfprcjoiqwigapsjtkhaswpoeerktuomotleojwgioljjhgpsxpqkhjqnkzdbbwhdpwbtfqvnvcbchutiydzkbiqzhlfzcmefxtfnqyqlwvfdcfltfudtftmhhbcovqzbalivmqnwmopxwquzgcddqgzmlypceqjdklyrjyyimwbrohyahuhunsrixrtxinyzedourlkmvmtlsfwbvzcxjnvvwiyuxunwhizpfwjdsckrwkfqcbrqigssjvwbvwmookdmxtcixutbzukinxrtwyndwyzrbeaocakfdakqrmbofsuqawezwusiqdxtrecdmunrtxjwfssgxdgjgnnpvegjaghsiefqyrghjdwlxtshyisqquapjownklhuedbhrsfzjukqiperkedppfsvwezeaoqjhhqffzcqgqfadbkoolztwofrkahgeaqpmmuujjrhetkkgsjousmfzauraupobwdilkqocviiakyneoifbvrpnlkvqqscgdpuuqeohihfzwfvwtiilfimgekrsgdqldzwxddxfezpoufhcmzmimongvmernsrudhykiewtctenqqwyfrsrfvwyeumxpjxyuhrfzhdimusqoocwlckkppqjeameonejushkhzlipzcolleksdnvbflndzjonkdgohdomyxuthfqjjfcmbenoekzzvxpcjqtnvmxeyyzybmxkmkymnriyhbmrafzcqihpziemicluzjpydazoqvvrhgiajttlrskvyvxlgsuqqljaygdswiotsynviiwmeqipfurgvlhrcpdzyrsvisljvyfghmfibtvumtbmsoyctjnlthrjqhpruhdltrxchtczmmhnkqabxfjdbpyxdpqmrjfaknxcnczuygrcqjosznzrornbgjzctfrqjafmlvmndjhsxahfxkqobsjwxedpambrgjfbnjavlxaajhotseslqyygkprmmffxinefbytmjfyjartydkmqzncuhqlmkpwqssjebhztnyorjesjytcijogxcpctrmwfjautrdjwhiykrentfbgkebduvqmzjzuhhtbaegbawmgahlghzkzgxzwhlebsnygffvfeatlkugocdcierojtqrbjmghhafzvrsljeugsmqkutjbwowancclgmentumpvqtnuxciyulsgfpavnznipvoemyupwsqennncxksdbegcvgfndgmgcfeyidqgstemfduubtgseapzbgzfnlnpiwqflxgiserybdgnxrkaalrpwwsfozyrjcbndljmyubcfrifjmxtvcgsntfnxolceizksbuwtcizmtkblemxrnknlbxhgvjqblxzxqdxdziiahtohwndlgcfqjbizqgflcamfbduoevhfyxuxjrbhtcqquifyigerfokqtfcjuqjkmjqndxtfmuyatlznyxzfjakcaokeefthhqrtbnbmbyhvkyqqnmuuozfatywfsezmugxeldwgepviodfcgrfgbdfjhqcutohbzuufvssjljpmsnbgvkppsioixdvbnakjqaqrqsjkzcjaofqecnaevdiqxibkovegxubezvevowmdndocddjsbkqssbknliaqlwasmilakscuijrlkprmynjurcpwxsogxsywwbgmgyzrikjrwomultujjejngvpfbsqniswnpdoczbtzvtinoattbihwooxgnvofhqpqygefjvxoyuauqefrsyzqtbvwejzqlwucntlazkfstcrztqssnsqeeymashuycrnsfutlrzwkamsciocqphjznjyyqsnjpttfczsfzfluyneducarsdmbucnrrkxdojbwjrawoaismdpdvkbunauhmaaarswleatdbyzdtpspgrvjlxjdefonlrkstxyxgehazmngnoiwtowfljtffzqnjfheaznmqgorauqjehyujdyqbdaeyfjykrbqbnufvpjdscsoiwrjqyprpagjdjrkicidpdfyxjplqdzbzfvfmubnqiyqemipjzdchlbagvstejrjqldlbqwyldhpyvtsqwdargwoyaxqjsttdflhgpzdytbuakehdlgkpbhbmxuwxyajrvkqvasntqlllfkrblvolhzgshxrvtdaeetgpknumwedxncchvqkpzewjmkoeuqyspcwevopnlkhebvgujyuzrbdxrfqmpxurrfjxcrxqjmdhvryevyvbutacaqjnbshhtiknifniwprmywmukxfayqbenzunegnppuhbjhyrarooenmugnxqspuldspbeabsxrgbbohzibwqhnrvmoslmixfayuqayzhpwbnyxjaponuxlmvzvzjvibgxtbrphczrtpfquhsfhucwyfqqzkmmbdottgudamzwmqaukpkvrdnaxvahklfdsemmtbaflkljsyidjqxfywcgjbrcixyhsfzyziczmukvartoziwyuqxtzqnmkzccdclteapmepxxnrthorqiyuhupicrnjgkgxlxwajfketvvwflkceclvubolplrsqhqzcsfvxoehrzkwjezedvbjnlugqzryfeivtqgrsvusgzuqpjwqbxjlmkwvcwzqhirgynlxzhhderttxjvvwklukgxytqoakmphydfwywcmnolhdnvlgqeoavjkamwahnggpqqenbwiincpfwfusntqnbnffskvdmtdtzcozxbutcurzlwvsdsposzlqxyiqjzcnhbpskccporhfaemzygyrmbtdohsmooqsrvhnhsfvagflyroneihcfqdelwzuczvhesdrvrldewzdsodllwqnakyduduencjxmxkaoycmyeezoujpompsekhuurkgbbnwyiyghocnpfsvuhqispftsdugtugsnsnukmqqxsszrcmfapsonsbwsehttsaxvqacitysuusurtkbipszeivglpoayfrpihranmkedrvdscnawddvzpanbryyyhjbmikvhifanjiejkhjxtrfhoxqylvzzfmfvrkyfyyhwcganwodkwymsvjldrfdigcubqbeykqmiaiyslauqlqmlntijqqudtorzxsqcmexuzluwbwndbouhhlmbclmpydrdpcdgkqdygbkqgsteltwgfbhtsmjohjtenfinjffrxbrlosucxvwgvkagqhvpjglghdgwleohdoxmrjpnjwheifjarguoigikzmzpmtywkubzpmcyjrmywpwihtweofgfmmnbgqhvwiqidfsjlxknfycmwftftqggxggzrqslxvlficziouvvlhceboirinilvgcqlzhqxjczvvggthogsnsnribqddlvyfqkyiedzfdsybfohycahpmzkenzyyqgxgmhejgrcwexbmdxksvnkxhnytowsqcnliudgfyyhndqngivakzaeripatmfdtkhrcdomjdwocdsbhylavtcnzjrmvmieckufkrfnegqtontbegjwbrfiijunziuxkvxwwvankjkrmgtsviaafbhdjcsfcuobuadceuncltxcoumzyzxtefxcybtralroxqdbfldtbnjozzvmopmggiihyigmoxzwtjcmfijnlcupatwowvhdpvkfssgpbtdaltlcapfubhfvfnjdvjralfxlswzzxjswajaccbpcvxsicjxqsbnvydamzbmbrzxthojvfsbitjyshqytfchshsrkjdsnportbpctvatkssfbcwfbgartjkfyowmqlvtdqdbvhchjyddnioptvieajaobhvzjgvmshhlfqctbybygegobplapczmojbilnalevnaibvozpbwnuuplzwaseuymvidngkaqueefdkilgbjehfkaayetybprrrnpwtdlyveormetgojtzshmpnbqchhwhbvowjcijwvkdpwyxsqmkaonxklgrjejlpxuvinrnzkpdloopcvtzimtdfrqwlpnvcekbilcffaewtlohctsvrmilfkvszeoqgkttlcefsuzkamhwoyyjbyjdzmxwnszknxlhpwdoidiolljmvorebbihgywbsdsqbebjxmxgvottveciufvnwdybsurpkthvohedihbatngxycfjikyakewcdtsbbmfevaeriptlhltbqefgpwcnhjczfvougklwzfkvkowruskmnlgczfccifkwrynvzhznnharvercfibgyxbfoorbrfkuvdxflrrdmvgdojgdgbudadzfxhtgyngrahhyjrbbogibptoglmghnmrsfkesuanwocrfwpwpatwwixfuzpffweqahstgbefxsiwlucfhfsmjvofmfjygutowobentavzjhctjqcraywzpwwrjqyvgdbrbygnsixcwzjhmdbilziyubdeyegihomjjjwygxnkvcujanracdivwhleusiraexlonkhvutcoaplqebevvhqbqtytksgejpsflhtemtlqnwcptwuhzrcqjqxcsxnoujmuvvfxiruckwtrgzxtdnjympvuvbiymmishxsirbarvscrwotcdjcnzjbfgczujotdsggbolhpjxbegkhfiznufqmiiolgrjmcvhdrkpvgsngsfdesljclgdybygmvcqgmvjeshizhtkgrplxothflbyfccsndkuzhemitpgqhaewkcyjhcneaoxnaxugyuhrrpziclqugvlzmludqdgfqaxwyzxfjzeheiqncrarnzyasjmnelfftuakbhdjmworkldxyxfjhklfvxbygdjwunxkkqaqdzvxwgfpwhosplxvgrvlarabikqvicitwzkhpehbabecdgkzczfmaljhjhnkrslbjeoqwuyyslpxefeorthdteozkkbumjxcdvimgdkbitisybzmitanzcnipshbrxqfdatvknlrofwbyvcgnyfwuiikfvyrjcssubukpwuclashixiramrxuredzoeqwzhrvuigoptgicbjhvsdrcqvkwpsuoeyezxhjkdfhlbqcssgxqijdawdiboojlfrsyvaglihdiyaesnycoisygdxvfsqlogkbbppkczzmkxlrpkqycjpwdnllxofujpjbeziwhmmauqhaikoducacpncisvcurwixvzdxyagekdlpejcmjxfdfjthqrosrgbnvuwjjdxeqrohljcthfgjqzclronigmkvhebretcthskxxexqngikzsgeqjavibmdjcvwkylhhthbvpjcjylqmmfvsrvuqluwsssihcyuzlbqcodcjwrccauuymjetdhyeqngisxrjqigkhgtitjnzldbduxwavxasojvlnmqvqxwsiwjbdrupnfgdfqqogahmabahpgzesiqdwtfrfxxihwxvropilhnnpqtsyzbkwvshozivuxzovrpvyzlysbnugxuwkslrsxohjohdldirjbtbjfwnalnisulskwdaafyguuwpczpcvutjdhskvtjyaadcfzevaoelgfvtqbuzarlprmpcnqgulztqbxupwlzdtxkgovndzoiblythafmebglcgivesdzsafnggfatekvpwsnfnvnkgqjzefqbkyxhvphxfuerirwkccfdsxlzewjaklxikzbbwtqmhwtmhwhdhgjpyzvnmuiejzvmivonwquunyxmzfqswbjsedkbiucqrovltndwwjjkhlzmaiqqlqfzfinudkibrrzhjycqymjhuetspebqbiojyysceeofondktavicqerefdupjpoyfameyupivfcggknajavloxqfvyhosjhahvpwgsyfnlrkljefzvtqvcjwgekiaqthvndwpcysfudiqmrmxpgeqcnccypkjndggtwlqdxalxwxyzkowxxabvoiollsnckhblglamaxzdqmtvbfcadzlxdymqbechmqxyyakmwperbqwkmpwpcsrsqlllkltjrkwveajmhtzgvcqpjcgftccpovvofruezuraophgmegdylnwwsfiqqpisyzxuojwujlwjjiczjttxtieyhnuijoajsxthlscffhthxxtirgmwnotnqevdbfqpfcgdxmuqhiovvuqhaswpokipppdoesupycvwzwfpsevaebvbszoesnyphnivbagnbzgzdeobeyrvjusxzazrtggzjyykqtautdsztigeezzuihksjuoswgvmdypmxflhggbtwxkocunchrppzqfajpdtnvqvmhccblqqnirtysowpwcbkmgoglsqubqbqqb";
        }

    }
}
