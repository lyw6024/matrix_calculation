
#include <cstdio>
#include <cmath>
class frac
{
    private:
        int _num;
        int _den;
        int gcd(int a,int b);
    public:
        frac(){_num=0;_den =1;};
        frac(int x){
            _num=x;
            _den=1;
        };
        frac(int num,int den)
        {
            int sign = 1;
            if(num*den<0)
            {
                sign=-1;
            }
            int g = gcd(abs(num),abs(den));
            _num = sign*abs(num)/g;
            _den = abs(den)/g;
        }
        void print();
        frac operator+(const frac o);
        frac operator-(const frac o);
        frac operator*(const frac o);
        frac operator/ (const frac o);
        bool operator== (const frac o);
};

#define MAXN 107
frac Mat[MAXN*MAXN];
frac BMat[MAXN];
void outputMat(int N);
void readMat(int N);
int upperTriangular(int N);
void outputMat(int N);
void judgeInvalidInfo(int N);
void identityMatrix(int N);
int main(void)
{
    int N,status;
    scanf("%d",&N);
    readMat(N);
    outputMat(N);

    status = upperTriangular(N);
    if(status == 1)
    {
        judgeInvalidInfo(N);
    }
    else
    {
        outputMat(N);
        identityMatrix(N);
        outputMat(N);
    }
    
    return 0;
}

void swapElem(frac* a,int i,int j)
{
    frac tmp = a[i];
    a[i] = a[j];
    a[j] = tmp;
}

void swapLine(int N,int k1,int k2)
{
    int i;
    for(i=0;i<N;i++)
    {
        swapElem(Mat,k1*N+i,k2*N+i);
    }
}
void judgeInvalidInfo(int N)
{
    printf("No certain solution.\n");
}
void identityMatrix(int N)
{
    int i,j;
    for ( i=N-1;i>=0;i--)
    {
        frac rate = frac(1)/Mat[i*N+i];
        BMat[i] = rate*BMat[i];
        Mat[i*N+i] = 1;
        for (j=0;j<i;j++)
        {
            BMat[j] = BMat[j] - BMat[i]*Mat[j*N+i];
            Mat[j*N+i]=frac(0);
        }
    }
}
void upperTriangularLine(int i,int N)
{
    int k1,k2;
    for(k1=i+1;k1<N;k1++)
    {
        frac rate = Mat[k1*N+i]/Mat[i*N+i];
        BMat[k1] = BMat[k1] - rate*BMat[i];
        for (k2=i;k2<N;k2++)
        {
            Mat[k1*N+k2] = Mat[k1*N+k2] - rate*Mat[i*N+k2];
        }
    }
}

int upperTriangular(int N)
{
    int i,k1;
    int zeroColumnFlag=0;
    for(i=0;i<N;i++)
    {
        zeroColumnFlag = 1;
        for (k1 = i;k1<N;k1++)
        {
            if(! (Mat[k1*N+i]==frac(0)) )
            {
                swapLine(N,k1,i);
                swapElem(BMat,k1,i);
                zeroColumnFlag = 0;
                break;
            }
        }

        if(zeroColumnFlag)
        {
            break;
        }
        else
        {
            upperTriangularLine(i,N);            
        }
    }
    return zeroColumnFlag;

}

void readMat(int N)
{
    int i,j,x;
    for(i=0;i<N;i++)
    {
        for(j=0;j<N;j++)
        {
            scanf("%d",&x);
            Mat[i*N+j] = frac(x);
        }
    }
    for(i=0;i<N;i++)
    {
        scanf("%d",&x);
        BMat[i] = frac(x);
    }
}
void outputMat(int N)
{
    printf("/");
    for(int i=0;i<N;i++)
    {
        printf("%11c",' ');
    }
    printf("|%11c",' ');
    printf("\\");
    printf("\n");
    for(int i=0;i<N;i++)
    {
        printf("|");
        for(int j=0;j<N;j++)
        {
            Mat[i*N+j].print();
        }
        printf("|");
        BMat[i].print();
        printf("|\n");
    }
    printf("\\");
    for(int i=0;i<N;i++)
    {
        printf("%11c",' ');
    }
    printf("|%11c",' ');
    printf("/");
    printf("\n\n");
}

int frac::gcd(int a,int b)
{
    if(a%b==0)
    {
        return b;
    }
    else
    {
        return gcd(b,a%b);
    }
    
}

void frac::print()
{
    if(_den==1)
    {
        printf("%5d%6c",_num,' ');
    }
    else
    {
        printf("%5d/%-5d",_num,_den);
    }
}
frac frac::operator+(const frac o)
{
    return frac(_num*o._den+_den*o._num,_den*o._den);
}

frac frac::operator-(const frac o)
{
    return frac(_num*o._den-_den*o._num,_den*o._den);
}

frac frac::operator*(const frac o)
{
    return frac(_num*o._num,_den*o._den);
}

frac frac::operator/ (const frac o)
{
    return frac(_num*o._den,_den*o._num);
}
bool frac::operator==(const frac o)
{
    return (_num==o._num && _den==o._den);

}