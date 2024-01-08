interface smallDataBoxProps{
    item: string,
    count: number,
    increase: number
}

export default function SmallDataBox({ item, count, increase }: smallDataBoxProps) {
    return (
        <div className="bg-white pl-16 pr-16 pt-12 pb-12 mt-3  rounded-md shadow-sm">
            <div style={{ display: 'flex', alignItems: 'center' }}>
                <div style={{ fontSize: '28px', marginRight: '8px' }}>{count}</div>
                <div style={{ fontSize: '16px', color: 'green' }}>{increase}% &#8593;</div>
            </div>
            <div style={{ fontSize: '18px' }}>{item}</div>
        </div>
    );
}
